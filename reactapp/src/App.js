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
                        updateQuantity={this.updateQuantity} />
                </div>
            </BrowserRouter>
        );
    }

    addToCart = (product) => {
        this.setState((prevState) => {
            const { cartItems, quantities } = prevState;
            const quantity = quantities[product.id] || 1;

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

}

export default App;
