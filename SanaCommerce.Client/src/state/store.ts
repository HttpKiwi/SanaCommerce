import { configureStore } from '@reduxjs/toolkit';
import productReducer from './product/productSlice.ts'
import cartReducer from './cart/cartSlice.ts'

const store = configureStore({
    reducer: {
        products: productReducer,
        cart: cartReducer,
    },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export {store};