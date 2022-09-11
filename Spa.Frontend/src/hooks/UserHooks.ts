import axios, { AxiosError } from 'axios';
import { useQuery } from 'react-query';
import config from '../config';
import { Claim } from '../types/claim';

const useFetchUser = () => {
  return useQuery<Claim[], AxiosError>('users', () =>
    axios
      .get(`${config.baseApiUrl}/account/getuser?slide=false`)
      .then((res) => res.data)
  );
};

export default useFetchUser;
