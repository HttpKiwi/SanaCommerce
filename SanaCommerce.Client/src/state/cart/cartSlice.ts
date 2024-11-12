import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface CartItem {
    id: string;
    name: string;
    quantity: number;
    price: number;
}

interface CartState {
    items: CartItem[];
    total: number;
}

const initialState: CartState = {
    items: JSON.parse(localStorage.getItem('cart') || '[]'),
    total: 0,
};

const cartSlice = createSlice({
    name: 'cart',
    initialState,
    reducers: {
        addItem: (state, action: PayloadAction<CartItem>) => {
            const existingItem = state.items.find(item => item.id === action.payload.id);
            if (existingItem) {
                existingItem.quantity += action.payload.quantity;
            } else {
                state.items.push(action.payload);
            }
            localStorage.setItem('cart', JSON.stringify(state.items));
        },
        removeItem: (state, action: PayloadAction<string>) => {
            state.items = state.items.filter(item => item.id !== action.payload);
            localStorage.setItem('cart', JSON.stringify(state.items));
        },
        clearCart: (state) => {
            state.items = [];
            localStorage.setItem('cart', JSON.stringify(state.items));
        },
    },
});

export const getCart = (state: { cart: CartState }) => state.cart;

export const { addItem, removeItem, clearCart } = cartSlice.actions;
export default cartSlice.reducer;