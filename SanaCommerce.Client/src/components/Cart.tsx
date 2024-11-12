import { Container, Drawer, List, ListItem, ListItemText } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import { getCart } from '../state/cart/cartSlice';

interface CartProps {
    open: boolean;
    toggleDrawer: () => void;
}

const Cart: React.FC<CartProps> = ({ open, toggleDrawer }) => {

    const cart = useSelector(getCart);

    return (
        <Drawer open={open} onClose={toggleDrawer} anchor='right'>
            <Container sx={{
                width: '20vw',
                display: 'flex',
                flexDirection: 'column',
                margin: '0 auto 0 auto',
            }}>
                <h2>Cart</h2>
                <List>
                    {cart.items.map((item, index) => (
                        <ListItem key={index}>
                            <ListItemText primary={item.name} secondary={`Quantity: ${item.quantity}`} />
                        </ListItem>
                    ))}
                </List>
            </Container>
        </Drawer>
    );
};

export default Cart;