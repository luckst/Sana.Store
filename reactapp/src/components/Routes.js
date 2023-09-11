// src/components/Routes.js
import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Cart from './Cart';
import Home from './Home';
import Products from './Products';

const Routing = (props) => {
    return (
        <Routes>
            <Route path="/" element={<Home />} />
            <Route
                path="/products"
                element={
                    <Products
                        quantities={props.quantities}
                        addToCart={props.addToCart}
                        increaseQuantity={props.increaseQuantity}
                        decreaseQuantity={props.decreaseQuantity}
                        updateQuantity={props.updateQuantity}
                    />
                }
            />
            <Route path="/cart"
                element={
                    <Cart cartItems={props.cartItems}
                        removeItem={props.removeItem}
                        removeAllItems={props.removeAllItems}
                        increaseQuantity={props.increaseQuantity}
                        decreaseQuantity={props.decreaseQuantity}
                        quantities={props.quantities}
                    />
                } />
            <Route path="*" element={<Home />} />
        </Routes>
    );
};

export default Routing;
