// src/components/Routes.js
import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Cart from './Cart';
import Home from './Home';
import Products from './Products';

const Routing = (props) => {
    console.log('Props en Routing:', props);
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
            <Route path="/cart" element={<Cart />} />
            <Route path="*" element={<Home />} />
        </Routes>
    );
};

export default Routing;