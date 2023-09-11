// src/components/Home.js
import React from 'react';
import { Link } from 'react-router-dom';

const Home = () => {
    return (
        <div>
            <h1>Welcome to the sana store app</h1>

            <p>
                To view our products please click here: <Link className="btn btn-primary" to="/products">Products</Link>
            </p>
        </div>
    );
};

export default Home;
