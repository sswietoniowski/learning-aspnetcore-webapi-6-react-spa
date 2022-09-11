import axios, { AxiosError, AxiosResponse } from 'axios';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import { useNavigate } from 'react-router-dom';
import config from '../config';
import { Login } from '../types/login';
import { Problem } from '../types/problem';

const useLoginUser = () => {
  const queryClient = useQueryClient();
  return useMutation<AxiosResponse, AxiosError<Problem>, Login>(
    (login: Login) =>
      axios
        .post(`${config.baseApiUrl}/auth/login`, login)
        .then((res) => res.data),
    {
      onSuccess: () => {
        queryClient.invalidateQueries('login');
      },
    }
  );
};

export default useLoginUser;
