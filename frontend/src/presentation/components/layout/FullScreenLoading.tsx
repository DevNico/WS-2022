import { CircularProgress } from '@mui/material';
import React from 'react';
import FullScreenCenter from './FullScreenCenter';

const FullScreenLoading: React.FC = () => {
	return (
		<FullScreenCenter>
			<CircularProgress />
		</FullScreenCenter>
	);
};

export default FullScreenLoading;
