import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';

const CartPage = () => {
  const [cartProducts, setCartProducts] = useState([]);

  useEffect(() => {
    axios
      .get('http://localhost:5193/Ecommers/cart')
      .then((response) => {
        setCartProducts(response.data);
      })
      .catch((error) => {
        console.error('Error al obtener los productos del carrito:', error);
      });
  }, []);

  return (
    <div>
      <h1>Shopping Cart</h1>
      {cartProducts.length === 0 ? (
        <p>Your cart is empty.</p>
      ) : (
        <ul>
          {cartProducts.map((product) => (
            <li key={product.id}>
              <div>
                <strong>{product.name}</strong> - ${product.price}
              </div>
            </li>
          ))}
          <Link to="/payment">
            <button>Pay</button>  
          </Link>
        </ul>
        
      )}
    </div>
  );
};

export default CartPage;
