import axios, { AxiosError, AxiosResponse } from 'axios';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import { useNavigate } from 'react-router-dom';
import config from '../config';
import { House } from '../types/house';
import { Problem } from '../types/problem';

const useFetchHouses = () => {
  return useQuery<House[], AxiosError>('houses', () =>
    axios.get(`${config.baseApiUrl}/houses`).then((res) => res.data)
  );
};

const useFetchHouse = (id: number) => {
  return useQuery<House, AxiosError>(['houses', id], () =>
    axios.get(`${config.baseApiUrl}/houses/${id}`).then((res) => res.data)
  );
};

const useAddHouse = () => {
  const cfg = {
    headers: { Authorization: `Bearer ${config.jwtToken}` },
  };

  const nav = useNavigate();
  const queryClient = useQueryClient();
  return useMutation<AxiosResponse, AxiosError<Problem>, House>(
    (house) =>
      axios
        .post(`${config.baseApiUrl}/houses`, house, cfg)
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
  const cfg = {
    headers: { Authorization: `Bearer ${config.jwtToken}` },
  };

  const nav = useNavigate();
  const queryClient = useQueryClient();
  return useMutation<AxiosResponse, AxiosError<Problem>, House>(
    (house) =>
      axios
        .put(`${config.baseApiUrl}/houses/${house.id}`, house, cfg)
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
  const cfg = {
    headers: { Authorization: `Bearer ${config.jwtToken}` },
  };

  const nav = useNavigate();
  const queryClient = useQueryClient();
  return useMutation<AxiosResponse, AxiosError, House>(
    (h) =>
      axios
        .delete(`${config.baseApiUrl}/houses/${h.id}`, cfg)
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
