import React, { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from "axios";
import './App.css';

function App() {
  const [products, setProducts] = useState([]);
  const history = useNavigate();

  const handleClick = () => {
    history('/category');
  };

  useEffect(() => {
    axios
        .get("http://localhost:5193/Ecommers/products")
        .then((response) => {
            setProducts(response.data);
        })
        .catch((error) => {
            console.error("Error al obtener los productos:", error);
        });
}, []);
const addToCart = (productId) => {
  axios.put(`http://localhost:5193/Ecommers/${productId}`)
    .then(response => {
      if (response.status === 204) {
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

      <button onClick={handleClick}>Go to Category Page</button>
      <Link to="/cart">
      <button style={{marginLeft: "10px"}}>Cart</button>
      </Link>
    </div>
  );
}

export default App;
