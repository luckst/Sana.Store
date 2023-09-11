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
                            <div className="row">
                                <div className="col-md-6">
                                    <h4 className="bold">{product.title}</h4>
                                    <div>
                                        <span style={{ color: "gray" }}>Item No. {product.code}</span> | <span style={{ color: "green" }}>{product.availableStock} in stock</span>
                                        <p style={{ color: "gray" }}>
                                            {product.description}
                                        </p>
                                    </div>
                                </div>
                                <div className="col-md-2 text-right">
                                   <span style={{ fontWeight: "bold" }}>${product.price}</span>
                                </div>
                                <div className="col-md-2">
                                    <form>
                                        <div className="form-row align-items-center">
                                            <div className="input-group">
                                                <div className="input-group-prepend">
                                                    <button type="button"  className="input-group-text btn btn-primary" onClick={() => this.props.decreaseQuantity(product.id)}>-</button>
                                                </div>
                                                <input
                                                    className="form-control"
                                                    type="number"
                                                    value={this.props.quantities[product.id] || 1}
                                                    onChange={(e) => this.props.updateQuantity(product.id, e.target.value)}
                                                    min="1"
                                                    readOnly
                                                />
                                                <div className="input-group-prepend">
                                                    <button type="button" className="input-group-text btn btn-primary" onClick={() => this.props.increaseQuantity(product.id, this.props.quantities[product.id] || 1)}>+</button>
                                                </div>
                                            </div>
                                        </div>
                                    </form>


                                </div>
                                <div className="col-md-2">
                                    <button className="btn btn-warning" onClick={() => this.props.addToCart(product)}>Add to Cart</button>
                                </div>
                            </div>
                        </li>
                    ))}
                </ul>
                {this.state.endReached && <div>No more products</div>}
                <div>
                    <button className="btn btn-warning" onClick={() => this.handlePagination('next')}>
                        Next page
                    </button>
                </div>
            </div>
        );
    }
}

export default Products;