import React, { Component } from 'react';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

class Products extends Component {
    constructor(props) {
        super(props);
        this.state = {
            products: [],
            pageNumber: 1,
            endReached: false
        };
    }

    componentDidMount() {
        fetch(`api/products?pageNumber=${this.state.pageNumber}`)
            .then((response) => {
                console.log(response);
                return response.json();
            })
            .then((data) => {
                this.setState({ products: data });
            })
            .catch((error) => {
                console.error('Error fetching data:', error);
            });
    }

    handlePagination = (direction) => {
        this.setState({
            pageNumber: this.state.pageNumber + (direction === 'next' ? 1 : -1),
        });
        this.handleScroll();
    };

    handleScroll = () => {
        if (!this.state.endReached) {
            this.fetchMoreProducts();
        }
    };

    fetchMoreProducts = () => {
        fetch(`api/products?pageNumber=${this.state.pageNumber + 1}`)
            .then((response) => { console.log(response); return response.json(); })
            .then((data) => {
                const newProducts = data;
                this.setState({
                    products: [...this.state.products, ...newProducts],
                    endReached: newProducts.length === 0,
                });
            })
            .catch((error) => {
                console.error('Error fetching data:', error);
            });;
    };

    render() {
        return (
            <div>
                <h1>Product list</h1>
                <ul style={{ listStyleType: 'none' }}>
                    {this.state.products.map((product) => (
                        <li key={product.id}>
                            <h4 className="bold">{product.title}</h4>
                            <div>
                                <span style={{ color: "gray" }}>Item No. {product.code}</span> | <span style={{ color: "green" }}>{product.availableStock} in stock</span>
                            </div>

                            <button onClick={() => this.props.decreaseQuantity(product.id)}>-</button>
                            <input
                                type="number"
                                value={this.props.quantities[product.id] || 1}
                                onChange={(e) => this.props.updateQuantity(product.id, e.target.value)}
                                min="1"
                            />
                            <button onClick={() => this.props.increaseQuantity(product.id, this.props.quantities[product.id] || 1)}>+</button>
                            <button onClick={() => this.props.addToCart(product)}>Add to Cart</button>
                        </li>
                    ))}
                </ul>
                {this.state.endReached && <div>No more products</div>}
                <div>
                    <button onClick={() => this.handlePagination('next')}>
                        Next
                    </button>
                </div>
            </div>
        );
    }
}

export default Products;