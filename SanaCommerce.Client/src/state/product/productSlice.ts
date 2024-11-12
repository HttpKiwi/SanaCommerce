import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export interface Product {
	productCode: string;
	name: string;
	price: number;
	stock: number;
	description: string;
	photo: string;
}

export interface ProductState {
	products: Product[];
	isLoading: boolean;
	error: string | undefined;
	selectedProduct: Product | null;
}

const initialState: ProductState = {
	products: [],
	isLoading: false,
	error: undefined,
	selectedProduct: null,
};

export const fetchProducts = createAsyncThunk('product/fetchProducts', async () => {
	const response = await axios.get('https://localhost:7136/api/Product');
	return [...response.data];
});

export const fetchProductById = createAsyncThunk('product/fetchProductById', async (id: string) => {
	const response = await axios.get(`https://localhost:7136/api/Product/${id}`);
	return response.data;
});

const productSlice = createSlice({
	name: 'product',
	initialState,
	reducers: {
		removeStock: () => {},
	},
	extraReducers: (builder) => {
		builder
			.addCase(fetchProducts.pending, (state) => {
				state.isLoading = true;
			})
			.addCase(fetchProducts.fulfilled, (state, action) => {
				state.isLoading = false;
				state.products = action.payload;
			})
			.addCase(fetchProducts.rejected, (state, action) => {
				state.isLoading = false;
				state.error = action.error.message;
			})
			.addCase(fetchProductById.pending, (state) => {
				state.isLoading = true;
			})
			.addCase(fetchProductById.fulfilled, (state, action) => {
				state.isLoading = false;
				state.selectedProduct = action.payload;
			})
			.addCase(fetchProductById.rejected, (state, action) => {
				state.isLoading = false;
				state.error = action.error.message;
			});
	},
});

export const getProducts = (state: { products: ProductState }) => state.products.products;
export const getSelectedProduct = (state: { products: ProductState }) => state.products.selectedProduct;

export const { removeStock } = productSlice.actions;
export default productSlice.reducer;
