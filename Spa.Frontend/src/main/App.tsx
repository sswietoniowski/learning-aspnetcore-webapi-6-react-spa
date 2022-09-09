import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';

import Header from './Header';
import HouseList from '../house/HouseList';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import HouseDetail from '../house/HouseDetail';

function App() {
  return (
    <BrowserRouter>
      <div className='container'>
        <Header subtitle='Providing houses all over the world' />
        <Routes>
          <Route path='/' element={<HouseList />} />
          <Route path='/houses/:id' element={<HouseDetail />} />
          <Route path='*' element={<h1>Not Found</h1>} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
