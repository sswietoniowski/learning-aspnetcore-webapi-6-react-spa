import { useParams } from 'react-router-dom';
import { currencyFormatter } from '../config';

const HouseDetails = () => {
  const { id } = useParams<{ id: string }>();
  // const { data, loading, error } = useQuery<HouseDetailsQuery, HouseDetailsQueryVariables>(HOUSE_DETAILS, {
  //     variables: { id },
  // });

  let data = null;

  // if (loading) {
  //     return <div>Loading...</div>;
  // }

  // if (error) {
  //     return <div>{error.message}</div>;
  // }

  return (
    <div>
      <div className='row mb-2'>
        <h5 className='themeFontColor text-center'>House Details</h5>
      </div>
      <table className='table table-hover'>
        <tbody>
          {/* <tr>
            <td>Address</td>
            <td>{data?.house.address}</td>
          </tr>
          <tr>
            <td>Country</td>
            <td>{data?.house.country}</td>
          </tr>
          <tr>
            <td>Asking Price</td>
            <td>{currencyFormatter.format(data?.house.price)}</td>
          </tr> */}
        </tbody>
      </table>
    </div>
  );
};

export default HouseDetails;
