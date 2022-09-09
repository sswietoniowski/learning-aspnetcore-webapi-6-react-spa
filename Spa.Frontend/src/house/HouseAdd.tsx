import { useAddHouse } from '../hooks/HouseHooks';
import { House } from '../types/house';
import HouseForm from './HouseForm';

const HouseAdd = () => {
  const addHouseMutation = useAddHouse();

  const house: House = {
    id: 0,
    address: '',
    country: '',
    description: '',
    price: 0,
    photo: '',
  };

  return (
    <HouseForm house={house} submitted={(h) => addHouseMutation.mutate(h)} />
  );
};

export default HouseAdd;
