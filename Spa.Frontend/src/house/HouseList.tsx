import { Link, useNavigate } from 'react-router-dom';
import { json } from 'stream/consumers';
import ApiStatus from '../ApiStatus';
import { currencyFormatter } from '../config';
import useFetchHouses from '../hooks/HouseHooks';
import useFetchUser from '../hooks/UserHooks';
import { House } from '../types/house';

const HouseList = () => {
  const nav = useNavigate();
  const { data, status, isSuccess } = useFetchHouses();
  const { data: userClaims } = useFetchUser();

  if (!isSuccess) {
    return <ApiStatus status={status} />;
  }

  return (
    <div>
      <div className='row mb-2'>
        <h5 className='themeFontColor text-center'>
          Houses currently on the market
        </h5>
      </div>
      <table className='table table-hover'>
        <thead>
          <tr>
            <th>Address</th>
            <th>Country</th>
            <th>Asking Price</th>
          </tr>
        </thead>
        <tbody>
          {data &&
            data.map((house: House) => (
              <tr key={house.id} onClick={() => nav(`/houses/${house.id}`)}>
                <td>{house.address}</td>
                <td>{house.country}</td>
                <td>{currencyFormatter.format(house.price)}</td>
              </tr>
            ))}
        </tbody>
      </table>
      {userClaims &&
        userClaims.find((c) => c.type === 'role' && c.value === 'admin') && (
          <Link className='btn btn-primary' to='/houses/add'>
            Add
          </Link>
        )}
    </div>
  );
};

export default HouseList;
