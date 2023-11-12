import React, { useState, useEffect } from 'react';
import axios from 'axios';

const ArtPage = () => {
  const [artProducts, setArtProducts] = useState([]);
  useEffect(() => {
    axios
      .get('http://localhost:5193/Ecommers')
      .then((response) => {
  
        const artProducts = response.data.filter((product) => product.category === 'art');
  
        setArtProducts(artProducts);
      })
      .catch((error) => {
        console.error('Error al obtener los productos:', error);
      });
  }, []);

  const addToCart = (productId) => {
    axios.post(`http://localhost:5193/Ecommers/${productId}`)
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
      <h1>Art Category</h1>
      <p>Explore our collection of art products.</p>
      <ul>
        {artProducts.map((product) => (
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

export default ArtPage;
