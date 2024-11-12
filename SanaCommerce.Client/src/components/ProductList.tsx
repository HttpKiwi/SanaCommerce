import React, { useEffect, useState } from 'react';
import { Container, Button, Divider } from '@mui/material';
import ProductCard from './ProductCard';
import { useDispatch, useSelector } from 'react-redux';
import { AppDispatch } from '../state/store';
import { fetchProducts, getProducts, Product } from '../state/product/productSlice';

const ProductList: React.FC = () => {
    const products: Product[] = useSelector(getProducts);
    const dispatch = useDispatch<AppDispatch>();

    const [currentPage, setCurrentPage] = useState(1);
    const productsPerPage = 10;

    const indexOfLastProduct = currentPage * productsPerPage;
    const indexOfFirstProduct = indexOfLastProduct - productsPerPage;
    const currentProducts = products.slice(indexOfFirstProduct, indexOfLastProduct);

    const handleNextPage = () => {
        console.log(currentPage);
        setCurrentPage(prevPage => prevPage + 1);
    };

    const handlePrevPage = () => {
        setCurrentPage(prevPage => prevPage - 1);
    };

    useEffect(() => {
        dispatch(fetchProducts());
    }, [dispatch]);

    return (
        <Container>
            {currentProducts.map(product => (
                <Container sx={{margin: '10px'}} key={product.productCode}>
                    <ProductCard
                        price={product.price.toString()}
                        title={product.name}
                        description={product.description}
                        productId={product.productCode}
                        stock={product.stock}
                        photo={product.photo}
                    />
                    <Divider variant="inset" />
                </Container>
            ))}
            <div>
                <Button
                    onClick={handlePrevPage}
                    disabled={currentPage === 1}
                >
                    Previous
                </Button>
                <Button
                    onClick={handleNextPage}
                    disabled={indexOfLastProduct >= products.length}
                >
                    Next
                </Button>
            </div>
        </Container>
    );
};

export default ProductList;