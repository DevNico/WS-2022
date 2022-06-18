import { CircularProgress } from '@mui/material';
import React from 'react';
import Center from './Center';

const FullScreenLoading: React.FC = () => {
	return (
		<Center>
			<CircularProgress />
		</Center>
	);
};

export default FullScreenLoading;
