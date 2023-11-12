import React, { useState, useEffect } from 'react';
import axios from 'axios';

const FantasyPage = () => {
  const [fantasyProducts, setFantasyProducts] = useState([]);
  useEffect(() => {
    axios
      .get('http://localhost:5193/Product')
      .then((response) => {
  
        const fantasyProducts = response.data.filter((product) => product.category === 'fantasy');
  
        setFantasyProducts(fantasyProducts);
      })
      .catch((error) => {
        console.error('Error al obtener los productos:', error);
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
    <div>
      <h1>Fantasy Category</h1>
      <p>Explore our collection of fantasy products.</p>
      <ul>
        {fantasyProducts.map((product) => (
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
};

export default FantasyPage;
