import React from 'react';
import { Container } from '@mui/material';
import Header from './components/Header.tsx';
import ProductList from './components/ProductList.tsx';


const OrdersList: React.FC = () => {
    return (
        <div>
            <Header/>
            <Container sx={{
                display: 'flex',
                flexDirection: 'column',
            }}>
                <Container>
                    <ProductList />
                </Container>
            </Container>
        </div>
    );
};

export default OrdersList;

