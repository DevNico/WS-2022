import Button from '@mui/material/Button/Button';
import Container from '@mui/material/Container/Container';
import Grid from '@mui/material/Grid/Grid';
import Typography from '@mui/material/Typography/Typography';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';
import FullScreenCenter from '../components/layout/FullScreenCenter';

const NotFoundPage: React.FC = () => {
	const { t } = useTranslation();

	return (
		<FullScreenCenter>
			<Container maxWidth='sm'>
				<Typography
					variant='h1'
					component='h2'
					textAlign='center'
					sx={{ mb: 2 }}
				>
					{t('notFoundPage.title')}
				</Typography>
				<Typography variant='body1' textAlign='center' sx={{ mb: 3 }}>
					{t('notFoundPage.subtitle')}
				</Typography>
				<Grid container justifyContent='center'>
					<Button component={Link} to={'/'}>
						{t('notFoundPage.goToHome')}
					</Button>
				</Grid>
			</Container>
		</FullScreenCenter>
	);
};

export default NotFoundPage;
