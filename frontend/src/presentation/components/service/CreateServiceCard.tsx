import AddIcon from '@mui/icons-material/Add';
import Card, { CardProps } from '@mui/material/Card/Card';
import CardActionArea from '@mui/material/CardActionArea/CardActionArea';
import CardContent from '@mui/material/CardContent/CardContent';
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography/Typography';
import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { OrganisationRecord } from '../../../api/models';
import Center from '../layout/Center';
import CreateServiceDialog from './CreateServiceDialog';

export interface CreateServiceCardProps extends CardProps {
	organisation: OrganisationRecord;
}

const CreateServiceCard: React.FC<CreateServiceCardProps> = ({
	organisation,
	...rest
}) => {
	const { t } = useTranslation();
	const [open, setOpen] = useState(false);

	const handleOpen = () => {
		setOpen(true);
	};

	const handleClose = () => {
		setOpen(false);
	};

	return (
		<>
			<Card variant='outlined' {...rest}>
				<CardActionArea onClick={handleOpen}>
					<CardContent>
						<Stack>
							<Center my={[1, 2]}>
								<Typography variant='h5' gutterBottom>
									{t('service.create.title')}
								</Typography>
								<AddIcon />
							</Center>
						</Stack>
					</CardContent>
				</CardActionArea>
			</Card>
			<CreateServiceDialog
				open={open}
				onClose={handleClose}
				organisation={organisation}
			/>
		</>
	);
};

export default CreateServiceCard;
