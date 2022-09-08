import axios, { AxiosError } from 'axios';
import { useQuery } from 'react-query';
import config from '../config';
import { House } from '../types/house';

const useFetchHouses = () => {
  return useQuery<House[], AxiosError>('houses', () =>
    axios.get(`${config.baseApiUrl}/houses`).then((res) => res.data)
  );
};

export default useFetchHouses;
