import { Bid } from './../types/bid';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import Config from '../config';
import axios, { AxiosError, AxiosResponse } from 'axios';
import { Problem } from '../types/problem';
import config from '../config';

const useFetchBids = (houseId: number) => {
  const cfg = {
    headers: { Authorization: `Bearer ${config.jwtToken}` },
  };

  return useQuery<Bid[], AxiosError>(['bids', houseId], () =>
    axios
      .get(`${Config.baseApiUrl}/house/${houseId}/bids`, cfg)
      .then((resp) => resp.data)
  );
};

const useAddBid = () => {
  const cfg = {
    headers: { Authorization: `Bearer ${config.jwtToken}` },
  };

  const queryClient = useQueryClient();
  return useMutation<AxiosResponse, AxiosError<Problem>, Bid>(
    (b) => axios.post(`${Config.baseApiUrl}/house/${b.houseId}/bids`, b, cfg),
    {
      onSuccess: (resp, bid) => {
        queryClient.invalidateQueries(['bids', bid.houseId]);
      },
    }
  );
};

export { useFetchBids, useAddBid };
