import { Container } from '@mui/material';
import React from 'react';
import { useDispatch } from 'react-redux';
import { addItem } from '../state/cart/cartSlice';

interface ProductCardProps {
    productId: string;
    title: string;
    price: string;
    description: string;
    stock: number;
    photo: string;
}

const ProductCard: React.FC<ProductCardProps> = ({ title, price, description, productId, stock, photo }) => {
    const [quantity, setQuantity] = React.useState(1);

    const dispatch = useDispatch();

    const handleIncrease = () => setQuantity(prev => prev + 1);
    const handleDecrease = () => setQuantity(prev => (prev > 1 ? prev - 1 : 1));

    

    const handleAddToCart = () => {
        dispatch(addItem({ id: productId, name: title, quantity, price: Number(price) }));
    }

    return (
        <Container sx={{
            display: 'grid',
            gridTemplateColumns: 'repeat(5, 1fr)', 
            gridTemplateRows: 'repeat(5, 1fr)',    
            gap: '8px',
            width: '60vw', 
            color: '#6e6e6e'
        }}>
            <div style={{
                gridColumn: 'span 3 / span 3', 
                gridRow: '1 / 2'               
            }}>
                <h2>{title}</h2>
            </div>

            <div style={{
                gridColumn: 'span 3 / span 3',  
                gridRow: '2 / span 3'         
            }}>
                <img src={`data:photo/jpeg;base64,${photo}`} alt={description} style={{ maxWidth: '300px' }} />
            </div>

            <div style={{
                gridColumn: 'span 3 / span 3', 
                gridRow: '5 / 6'             
            }}>
                <p>Item No. {productId} {stock > 0 ? `Stock: ${stock}` : "out of stock"}</p>
                <p>{description}</p>
            </div>

            <div style={{
                gridColumnStart: '4',          
                gridRowStart: '3',             
                justifyContent: 'center',
                display: 'flex',
                alignItems: 'center',
                fontWeight: 'bold'
            }}>
                <p style={{ fontSize: '1.5rem' }}>${price}</p>
            </div>

            {/* Quantity Controls and Add to Cart Button */}
            <div style={{
                gridColumnStart: '5',          
                gridRowStart: '3',              
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center'
            }}>
                <div style={{ display: 'flex', alignItems: 'center', marginBottom: '1rem' }}>
                    <button onClick={handleDecrease} style={{ padding: '0.5rem', backgroundColor: '#f9f9f9', color: '#6e6e6e', border: 'none' }}>-</button>
                    <input
                        type="number"
                        value={quantity}
                        onChange={(e) => setQuantity(Number(e.target.value))}
                        style={{
                            width: '3rem', textAlign: 'center', MozAppearance: 'textfield',
                            WebkitAppearance: 'textfield', margin: '0 0.5rem'
                        }}
                        min="1"
                    />
                    <button onClick={handleIncrease} style={{ padding: '0.5rem', backgroundColor: '#f9f9f9', color: '#6e6e6e', border: 'none' }}>+</button>
                </div>
                <button onClick={handleAddToCart} style={{ padding: '1rem', backgroundColor: '#f9f9f9', color: '#6e6e6e', border: 'none' }}>Add to cart</button>
            </div>
        </Container>
    );
};

export default ProductCard;
