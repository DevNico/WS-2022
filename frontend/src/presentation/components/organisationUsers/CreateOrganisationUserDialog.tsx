import Dialog from '@mui/material/Dialog/Dialog';
import DialogContent from '@mui/material/DialogContent/DialogContent';
import DialogTitle from '@mui/material/DialogTitle/DialogTitle';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { OrganisationRecord } from '../../../api/models';
import OrganisationUserForm from './OrganisationUserForm';

export interface CreateOrganisationUserDialogProps {
	organisation: OrganisationRecord;
	open: boolean;
	onClose: () => void;
}

const CreateOrganisationUserDialog: React.FC<
	CreateOrganisationUserDialogProps
> = ({ organisation, open, onClose }) => {
	const { t } = useTranslation();

	return (
		<Dialog open={open} onClose={onClose} keepMounted>
			<DialogTitle>{t('organisation.users.create.title')}</DialogTitle>
			<DialogContent>
				<OrganisationUserForm
					organisation={organisation}
					onSubmitSuccess={onClose}
				/>
			</DialogContent>
		</Dialog>
	);
};

export default CreateOrganisationUserDialog;
