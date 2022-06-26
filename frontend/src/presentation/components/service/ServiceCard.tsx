import Card, { CardProps } from '@mui/material/Card/Card';
import CardContent from '@mui/material/CardContent/CardContent';
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography/Typography';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { ServiceRecord } from '../../../api/models';
import { homeRoute } from '../../Router';
import RouterCardActionArea from '../RouterCardActionArea';

export interface ServiceCardProps extends CardProps {
	service: ServiceRecord;
}

const ServiceCard: React.FC<ServiceCardProps> = ({ service, ...rest }) => {
	const { t } = useTranslation();

	return (
		<Card variant='outlined' {...rest}>
			<RouterCardActionArea
				to={homeRoute({}).service({ name: service.routeName! })}
				{...rest}
			>
				<CardContent>
					<Stack>
						<Typography variant='h5' gutterBottom>
							{service.name!}
						</Typography>
						<Typography variant='caption'>
							{t('services.model.updatedAt', {
								updatedAt: new Date(
									service.updatedAt!
								).toLocaleDateString(),
								interpolation: { escapeValue: false },
							})}
						</Typography>
					</Stack>
				</CardContent>
			</RouterCardActionArea>
		</Card>
	);
};

export default ServiceCard;
