import React from 'react';
import { createRoot } from 'react-dom';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import App from './App';
import CategoryPage from './CategoryPage';
import HistoryPage from './History';
import ArtPage from './Art';
import FantasyPage from './Fantasy';
import CartPage from './CartPage';
import PaymentPage from './PaymentPage';

createRoot(document.getElementById('root')).render(
  <Router>
    <Routes>
      <Route path="/" element={<App />} />
      <Route path="/category" element={<CategoryPage />} />
      <Route path="/category/history" element={<HistoryPage />} />
      <Route path="/category/art" element={<ArtPage />} />
      <Route path="/category/fantasy" element={<FantasyPage />} />
      <Route path="/cart" element={<CartPage />} />
      <Route path="/payment" element={<PaymentPage />} />
    </Routes>
  </Router>
);
