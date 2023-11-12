import React from 'react';
import { Link } from 'react-router-dom';

const CategoryPage = () => {
  return (
    <div>
      <h1>Categories</h1>
      <div>
        <Link to="/category/history">
          <button style={{marginRight: "10px"}}>History</button>
        </Link>
        <Link to="/category/art">
          <button style={{marginRight: "10px"}}>Art</button>
        </Link>
        <Link to="/category/fantasy">
          <button style={{marginRight: "10px"}}>Fantasy</button>
        </Link>
      </div>
      
    </div>
  );
};

export default CategoryPage;
