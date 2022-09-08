import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';

import Header from './Header';
import HouseList from '../house/HouseList';

function App() {
  return (
    <div className='container'>
      <Header subtitle='Providing houses all over the world' />
      <HouseList />
    </div>
  );
}

export default App;
