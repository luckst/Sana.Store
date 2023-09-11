import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash } from '@fortawesome/free-solid-svg-icons';

const Cart = ({ cartItems, removeItem, removeAllItems, decreaseQuantity, increaseQuantity, updateQuantity, quantities }) => {
    console.log('Quantities en cart:', quantities);
    console.log('Cart items en cart:', cartItems);
    const navigate = useNavigate();

    const createOrderModel = cartItems.map((item) => {
        return {
            productId: item.id,
            quantity: item.quantity,
            price: item.price
        };
    });

    const calculateItemTotal = (item) => {
        return item.price * item.quantity;
    };

    const calculateTotalItems = () => {
        return cartItems.reduce((acc, item) => acc + item.quantity, 0);
    };

    const calculateTotalPrice = () => {
        return cartItems.reduce((acc, item) => acc + calculateItemTotal(item), 0);
    };

    const handleProcessOrder = async () => {
        const isOrderSaved = await saveOrder({
            details: createOrderModel
        });
        if (isOrderSaved) {
            removeAllItems();
            navigate('/');
        }
    };

    const saveOrder = async (model) => {
        try {
            const response = await fetch(`api/orders`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(model)
            });

            if (response.ok) {
                alert("Order created");
                return true;
            } else {
                console.error('Error:', response.statusText);
                return false;
            }
        } catch (error) {
            console.error('Error creating the order:', error);
            return false;
        }
    };

    return (
        <div className="cart">
            <div className="row">
                {cartItems.length === 0 && (
                    <p>There are no items in the cart. To view our products, please click here: <Link className="btn btn-primary" to="/products">Products</Link></p>
                )}
                <div className="col-md-8 table-responsive">
                    <h2>My shopping Cart</h2>
                    {cartItems.length > 0 && (
                        <table className="table table-hover">
                            <thead>
                                <tr>
                                    <td>Product</td>
                                    <td>Price</td>
                                    <td>Quantity</td>
                                    <td>Total</td>
                                </tr>
                            </thead>
                            <tbody>
                                {cartItems.map((item) => (
                                    <tr key={item.id}>
                                        <td valign="middle">
                                            <h5 className="bold">{item.title}</h5>
                                            <span style={{ color: "gray" }}>Item No. {item.code}</span><br />
                                            <a href="#" className="btn btn-link" onClick={() => removeItem(item.id)}><FontAwesomeIcon icon={faTrash} />Remove</a>
                                        </td>
                                        <td valign="middle">
                                            <span>$ {item.price}</span>
                                        </td>
                                        <td valign="middle">
                                            <form>
                                                <div className="form-row align-items-center">
                                                    <div className="input-group">
                                                        <div className="input-group-prepend">
                                                            <button type="button" className="input-group-text btn btn-primary" onClick={() => decreaseQuantity(item.id)}>-</button>
                                                        </div>
                                                        <input
                                                            className="form-control"
                                                            type="number"
                                                            value={quantities[item.id] || 1}
                                                            onChange={(e) => updateQuantity(item.id, e.target.value)}
                                                            min="1"
                                                            readOnly
                                                        />
                                                        <div className="input-group-prepend">
                                                            <button type="button" className="input-group-text btn btn-primary" onClick={() => increaseQuantity(item.id, quantities[item.id] || 1)}>+</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </form>
                                        </td>
                                        <td valign="middle">
                                            <span>$ {calculateItemTotal(item)}</span>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    )}
                </div>
                <div className="col-md-4" style={{ borderLeft: "1px solid light-gray" }}>
                    <h2>Shopping Cart details</h2>
                    <ul style={{ listStyleType: 'none' }}>
                        <li>
                            <div className="row">
                                <div className="col-md-6">
                                    <span>Items ({calculateTotalItems()})</span>
                                </div>
                                <div className="col-md-6">
                                    <span>$ {calculateTotalPrice()}</span>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div className="row">
                <div className="col-md-12 d-flex justify-content-end">
                    <button type="button" className="btn btn-primary" disabled={cartItems.length === 0} onClick={handleProcessOrder}>Process Order</button>
                </div>
            </div>
        </div>
    );
};

export default Cart;
