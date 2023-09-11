// src/components/Header.js
import React from 'react';
import { Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faShoppingCart } from '@fortawesome/free-solid-svg-icons';

const Header = ({ cartItemCount }) => {
    console.log("Props in header: ", cartItemCount);
    return (
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
            <Link className="navbar-brand" to="/">Sana Store</Link>
            <ul className="navbar-nav ml-auto">
                <li className="nav-item">
                    <Link className="nav-link" to="/">Home</Link>
                </li>
                <li className="nav-item">
                    <Link className="nav-link" to="/cart">
                        <FontAwesomeIcon icon={faShoppingCart} />
                        <span className="badge badge-primary">{cartItemCount}</span>
                    </Link>
                </li>
                <li className="nav-item">
                    {cartItemCount}

                </li>
            </ul>
        </nav>
    );
};

export default Header;
