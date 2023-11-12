import React, { useState, useEffect } from 'react';
import axios from 'axios';

const PaymentPage = () => {
  const [totalPrice, setTotalPrice] = useState([]);

  useEffect(() => {
    axios
      .get('https://ecommersback.azurewebsites.net/Ecommers/pay')
      .then((response) => {
        setTotalPrice(response.data);
      })
      .catch((error) => {
        console.error('Error al obtener el precio total:', error);
      });
  }, []);

  return (
    <div>
      <h1>Payment</h1>
      <p>Total Price: {totalPrice}</p>
    </div>
  );
};

export default PaymentPage;
