import axios, { AxiosError, AxiosResponse } from 'axios';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import { useNavigate } from 'react-router-dom';
import config from '../config';
import { House } from '../types/house';
import { Problem } from '../types/problem';

const useFetchHouses = () => {
  return useQuery<House[], AxiosError>('houses', () =>
    axios.get(`${config.baseApiUrl}/api/houses`).then((res) => res.data)
  );
};

const useFetchHouse = (id: number) => {
  return useQuery<House, AxiosError>(['houses', id], () =>
    axios.get(`${config.baseApiUrl}/api/houses/${id}`).then((res) => res.data)
  );
};

const useAddHouse = () => {
  const nav = useNavigate();
  const queryClient = useQueryClient();
  return useMutation<AxiosResponse, AxiosError<Problem>, House>(
    (house) =>
      axios
        .post(`${config.baseApiUrl}/api/houses`, house)
        .then((res) => res.data),
    {
      onSuccess: () => {
        queryClient.invalidateQueries('houses');
        nav('/');
      },
    }
  );
};

const useUpdateHouse = () => {
  const nav = useNavigate();
  const queryClient = useQueryClient();
  return useMutation<AxiosResponse, AxiosError<Problem>, House>(
    (house) =>
      axios
        .put(`${config.baseApiUrl}/api/houses/${house.id}`, house)
        .then((res) => res.data),
    {
      onSuccess: (_, house) => {
        queryClient.invalidateQueries('houses');
        nav(`/houses/${house.id}`);
      },
    }
  );
};

const useDeleteHouse = () => {
  const nav = useNavigate();
  const queryClient = useQueryClient();
  return useMutation<AxiosResponse, AxiosError, House>(
    (h) =>
      axios
        .delete(`${config.baseApiUrl}/api/houses/${h.id}`)
        .then((res) => res.data),
    {
      onSuccess: () => {
        queryClient.invalidateQueries('houses');
        nav('/');
      },
    }
  );
};

export default useFetchHouses;
export { useFetchHouse, useAddHouse, useUpdateHouse, useDeleteHouse };
