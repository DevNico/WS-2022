import Button from '@mui/material/Button/Button';
import Typography from '@mui/material/Typography/Typography';
import React from 'react';
import { useTranslation } from 'react-i18next';
import Center from './Center';

export interface FullScreenErrorProps {
	error: string;
	retry?: () => void;
}

const FullScreenError: React.FC<FullScreenErrorProps> = ({ error, retry }) => {
	const { t } = useTranslation();

	return (
		<Center>
			<Typography variant='h5'>{error}</Typography>
			{retry && <Button onClick={retry}>{t('common.retry')}</Button>}
		</Center>
	);
};

export default FullScreenError;
