// src/components/Layout.js
import React from 'react';
import Header from './Header';

const Layout = ({ cartItemCount }) => {
    console.log("Props in layout: ", cartItemCount);
    return (
        <div className="container-fluid">
            <Header cartItemCount={cartItemCount} />
        </div>
    );
};

export default Layout;
