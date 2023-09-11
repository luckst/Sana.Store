import React, { Component } from 'react';
import { BrowserRouter } from 'react-router-dom';
import Layout from './components/Layout';
import Routing from './components/Routes';

class App extends Component {
    constructor(props) {
        super(props);
        this.state = {
            cartItems: [],
            quantities: {},
            cartItemCount: 0
        };
    }

    getCartItemCount = () => {
        this.setState({
            cartItemCount: this.state.cartItems ? this.state.cartItems.length : 0,
        });
    };

    render() {
        
        return (
            <BrowserRouter>
                <div className="App">
                    <Layout cartItemCount={this.state.cartItemCount} />
                    <Routing quantities={this.state.quantities}
                        addToCart={this.addToCart}
                        increaseQuantity={this.increaseQuantity}
                        decreaseQuantity={this.decreaseQuantity}
                        updateQuantity={this.updateQuantity}
                        cartItems={this.state.cartItems}
                        removeItem={this.removeItem}
                        removeAllItems={this.removeAllItems}
                    />
                </div>
            </BrowserRouter>
        );
    }

    validateStock = async (productId, quantity) => {
        try {
            const response = await fetch(`api/products/${productId}/stockavailable/${quantity}`);
            const result = await response.json();
            return result.isAvailable;
        } catch (error) {
            console.error("Error checking stock availability:", error);
            return false;
        }
    }

    addToCart = async (product) => {
        const { cartItems, quantities } = this.state;
        const quantity = quantities[product.id] || 1;

        const isAvailable = await this.validateStock(product.id, quantity);

        if (!isAvailable) {
            alert("There's not enough stock available");
            return;
        }

        this.setState((prevState) => {
            const existingCartItem = cartItems.find((item) => item.id === product.id);

            if (existingCartItem) {
                const updatedCartItems = cartItems.map((item) => {
                    if (item.id === product.id) {
                        return {
                            ...item,
                            quantity: item.quantity + quantity,
                        };
                    }
                    return item;
                });
                return {
                    cartItems: updatedCartItems,
                };
            } else {
                return {
                    cartItems: [
                        ...cartItems,
                        {
                            ...product,
                            quantity,
                        },
                    ],
                };
            }
        }, () => {
            this.getCartItemCount();
        });
    };

    increaseQuantity = (productId, currentQuantity) => {
        const newQuantity = parseInt(currentQuantity, 10) + 1;

        this.setState((prevState) => ({
            quantities: {
                ...prevState.quantities,
                [productId]: newQuantity,
            },
        }));
    };

    decreaseQuantity = (productId) => {
        this.setState((prevState) => ({
            quantities: {
                ...prevState.quantities,
                [productId]: Math.max(1, (prevState.quantities[productId] || 0) - 1),
            },
        }));
    };

    updateQuantity = (productId, newQuantity) => {
        this.setState((prevState) => ({
            quantities: {
                ...prevState.quantities,
                [productId]: parseInt(newQuantity),
            },
        }));
    };

    removeItem = (productId) => {
        this.setState((prevState) => {
            const updatedCartItems = prevState.cartItems.filter((item) => item.id !== productId);
            const updatedQuantities = { ...prevState.quantities };
            delete updatedQuantities[productId];

            return {
                cartItems: updatedCartItems,
                quantities: updatedQuantities,
            };
        }, () => {
            this.getCartItemCount();
        });
    };

    removeAllItems = () => {
        this.setState({
            cartItems: [],
            quantities: {}, // Remove all quantities
        }, () => {
            this.getCartItemCount();
        });
    };

}

export default App;
