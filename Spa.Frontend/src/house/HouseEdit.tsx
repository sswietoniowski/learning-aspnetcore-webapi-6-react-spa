import { useParams } from 'react-router-dom';
import ApiStatus from '../ApiStatus';
import { useFetchHouse, useUpdateHouse } from '../hooks/HouseHooks';
import HouseForm from './HouseForm';

const HouseEdit = () => {
  const updateHouseMutation = useUpdateHouse();

  const { id } = useParams();
  if (!id) {
    throw Error('No id provided');
  }
  const houseId = parseInt(id);
  const { data, status, isSuccess } = useFetchHouse(houseId);

  if (!isSuccess) {
    return <ApiStatus status={status} />;
  }
  if (!data) {
    return <div>House not found</div>;
  }

  return (
    <HouseForm house={data} submitted={(h) => updateHouseMutation.mutate(h)} />
  );
};

export default HouseEdit;
