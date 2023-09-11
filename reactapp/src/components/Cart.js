import React from 'react';

const Cart = ({ cartItems }) => {
    return (
        <div className="cart">
            <h2>Shopping Cart</h2>
            <ul>
                {cartItems.map((item) => (
                    <li key={item.id}>
                        <span>{item.name}</span>
                        <span>Quantity: {item.quantity}</span>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Cart;