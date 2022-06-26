import Checkbox from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';
import Grid from '@mui/material/Grid';
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { OrganisationRoleRecord } from '../../../api/models';

export interface OrganisationRoleDetailsProps {
	role: OrganisationRoleRecord | undefined;
}

const OrganisationRoleDetails: React.FC<OrganisationRoleDetailsProps> = ({
	role,
}) => {
	const { t } = useTranslation();

	return (
		<Grid container spacing={3}>
			<Grid item sm={6} xs={12}>
				<Stack>
					<Typography variant='body2'>
						{t('organisation.roles.create.service')}
					</Typography>
					<FormControlLabel
						control={
							<Checkbox
								id='serviceWrite'
								checked={role?.serviceWrite ?? false}
							/>
						}
						label={t('organisation.roles.model.serviceWrite')}
						disabled
					/>
					<FormControlLabel
						control={
							<Checkbox
								id='serviceDelete'
								checked={role?.serviceDelete ?? false}
							/>
						}
						label={t('organisation.roles.model.serviceDelete')}
						disabled
					/>
				</Stack>
			</Grid>
			<Grid item sm={6} xs={12}>
				<Stack>
					<Typography variant='body2'>
						{t('organisation.roles.create.user')}
					</Typography>
					<FormControlLabel
						control={
							<Checkbox
								id='userRead'
								checked={role?.userRead ?? false}
							/>
						}
						label={t('organisation.roles.model.userRead')}
						disabled
					/>
					<FormControlLabel
						control={
							<Checkbox
								id='userWrite'
								checked={role?.userWrite ?? false}
							/>
						}
						label={t('organisation.roles.model.userWrite')}
						disabled
					/>
					<FormControlLabel
						control={
							<Checkbox
								id='userDelete'
								checked={role?.userDelete ?? false}
							/>
						}
						label={t('organisation.roles.model.userDelete')}
						disabled
					/>
				</Stack>
			</Grid>
		</Grid>
	);
};

export default OrganisationRoleDetails;
