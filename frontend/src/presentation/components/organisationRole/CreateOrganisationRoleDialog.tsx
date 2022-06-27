import Dialog from '@mui/material/Dialog/Dialog';
import DialogContent from '@mui/material/DialogContent/DialogContent';
import DialogTitle from '@mui/material/DialogTitle/DialogTitle';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { OrganisationRecord } from '../../../api/models';
import OrganisationRoleForm from './OrganisationRoleForm';

export interface CreateOrganisationRoleDialogProps {
	organisation: OrganisationRecord;
	open: boolean;
	onClose: () => void;
}

const CreateOrganisationRoleDialog: React.FC<
	CreateOrganisationRoleDialogProps
> = ({ organisation, open, onClose }) => {
	const { t } = useTranslation();

	return (
		<Dialog open={open} onClose={onClose}>
			<DialogTitle>{t('organisationRole.create.title')}</DialogTitle>
			<DialogContent>
				<OrganisationRoleForm
					organisation={organisation}
					onSubmitSuccess={onClose}
				/>
			</DialogContent>
		</Dialog>
	);
};

export default CreateOrganisationRoleDialog;
