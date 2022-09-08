type Args = {
  status: 'idle' | 'loading' | 'error' | 'success';
};

const ApiStatus = ({ status }: Args) => {
  switch (status) {
    case 'idle':
      return <div className='alert alert-info'>Idle</div>;
    case 'loading':
      return <div className='alert alert-info'>Loading...</div>;
    case 'error':
      return (
        <div className='alert alert-danger'>
          Error communicating with the data backend
        </div>
      );
    case 'success':
      return (
        <div className='alert alert-success'>Data fetched successfully</div>
      );
    default:
      throw Error(`Unknown API state: ${status}`);
  }
};

export default ApiStatus;
