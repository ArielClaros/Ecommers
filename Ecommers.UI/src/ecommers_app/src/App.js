import React, { useState, useEffect } from 'react';
import axios from "axios";
import './App.css';

function App() {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    axios
        .get("http://localhost:5193/Product")
        .then((response) => {
            setProducts(response.data);
        })
        .catch((error) => {
            console.error("Error al obtener los productos:", error);
        });
}, []);
const addToCart = (productId) => {
  axios.post(`http://localhost:5193/Booking/AddToCart?productId=${productId}&userName=Ari`)
    .then(response => {
      if (response.status === 200) {
        console.log('Product added to cart successfully.');
      } else {
        console.error('Error adding to cart:', response.statusText);
      }
    })
    .catch(error => console.error('Error adding to cart:', error));
};

  return (
    <div className="App">
      <h1>Product List</h1>
      <ul>
        {products.map(product => (
          <li key={product.id}>
            <div>
              <strong>{product.name}</strong> - ${product.price}
            </div>
            <button onClick={() => addToCart(product.id)}>Add to Cart</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;
