import React, { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from "axios";
import './App.css';

function App() {
  const [products, setProducts] = useState([]);
  const history = useNavigate();
  const [newProductName, setNewProductName] = useState('');
  const [newProductPrice, setNewProductPrice] = useState(0);
  const [newProductStock, setNewProductStock] = useState(0);
  const [newProductCategory, setNewProductCategory] = useState('');
  const [newProductCountInCart, setNewProductCountInCart] = useState(0);
  const [showAddProductPopup, setShowAddProductPopup] = useState(false);
  const showPopup = () => setShowAddProductPopup(true);
  const hidePopup = () => setShowAddProductPopup(false);

  const renderAddProductPopup = () => {
    return (
      <div className="popup">
        <h2>Add New Product</h2>
        <label>Name:</label>
        <input type="text" value={newProductName} onChange={(e) => setNewProductName(e.target.value)} />
        <label>Price:</label>
        <input type="number" value={newProductPrice} onChange={(e) => setNewProductPrice(Number(e.target.value))} />
        <label>Stock:</label>
        <input type="number" value={newProductStock} onChange={(e) => setNewProductStock(Number(e.target.value))} />
        <label>Category:</label>
        <input type="text" value={newProductCategory} onChange={(e) => setNewProductCategory(e.target.value)} />
        <label>CountInCart:</label>
        <input type="text" value={newProductCountInCart} onChange={(e) => setNewProductCountInCart(e.target.value)} />
        <button onClick={addNewProduct}>Create Product</button>
        <button onClick={hidePopup}>Cancel</button>
      </div>
    );
  };

  const handleClick = () => {
    history('/category');
  };

  useEffect(() => {
    axios
        .get("https://ecommersback.azurewebsites.net/Ecommers/products")
        .then((response) => {
            setProducts(response.data);
        })
        .catch((error) => {
            console.error("Error al obtener los productos:", error);
        });
}, []);

const addProduct = async (product) => {
  try {
    const response = await axios.post('https://ecommersback.azurewebsites.net/Ecommers', {
      Name: product.Name,
      Price: product.Price,
      Stock: product.Stock,
      Category: product.Category,
      CountInCart: product.CountInCart,
    });

    console.log('Product created:', response.data);
  } catch (error) {
    console.error('Error creating product:', error);
  }
};

const addNewProduct = async () => {
  const newProduct = {
    Name: newProductName,
    Price: newProductPrice,
    Stock: newProductStock,
    Category: newProductCategory,
    CountInCart: newProductCountInCart,
  };

  await addProduct(newProduct);  // Llamada a la función existente.

  // Limpiar campos y ocultar el pop-up después de agregar el producto.
  setNewProductName('');
  setNewProductPrice(0);
  setNewProductStock(0);
  setNewProductCategory('');
  hidePopup();
};

const addToCart = (productId) => {
  axios.put(`https://ecommersback.azurewebsites.net/Ecommers/${productId}`)
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
      <button onClick={showPopup}>Add Product</button>
      {showAddProductPopup && renderAddProductPopup()}
      <Link to="/cart">
      <button style={{marginLeft: "10px"}}>Cart</button>
      </Link>
    </div>
  );
}

export default App;
