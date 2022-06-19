import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import ListItemButton from '@mui/material/ListItemButton/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon/ListItemIcon';
import { CorporateFare } from '@mui/icons-material';
import ListItemText from '@mui/material/ListItemText/ListItemText';
import { ExpandLess, ExpandMore } from '@mui/icons-material';
import { Collapse } from '@mui/material';
import List from '@mui/material/List/List';

const OrganisationsListItem: React.FC<{ isSuperAdmin: boolean }> = ({
	isSuperAdmin,
}) => {
	const [open, setOpen] = useState(true);
	const navigate = useNavigate();
	const { t } = useTranslation();
	const location = useLocation();

	const isOrganisationCreatePage =
		location.pathname === '/organisations/create';
	const isOrganisationsPage = location.pathname === '/organisations';

	return (
		(isSuperAdmin && (
			<>
				<ListItemButton onClick={() => setOpen(!open)}>
					<ListItemIcon>
						<CorporateFare />
					</ListItemIcon>
					<ListItemText primary={t('homeLayout.organisations')} />
					{open ? <ExpandLess /> : <ExpandMore />}
				</ListItemButton>
				<Collapse in={open} timeout='auto' unmountOnExit>
					<List component='div' disablePadding>
						<ListItemButton
							sx={{ pl: 4 }}
							selected={isOrganisationsPage}
							onClick={() => navigate('/organisations')}
						>
							<ListItemIcon>
								<CorporateFare />
							</ListItemIcon>
							<ListItemText
								primary={t('homeLayout.listOrganisations')}
							/>
						</ListItemButton>
						<ListItemButton
							sx={{ pl: 4 }}
							selected={isOrganisationCreatePage}
							onClick={() => navigate('/organisations/create')}
						>
							<ListItemIcon>
								<CorporateFare />
							</ListItemIcon>
							<ListItemText
								primary={t('homeLayout.createOrganisation')}
							/>
						</ListItemButton>
					</List>
				</Collapse>
			</>
		)) || <></>
	);
};

export default OrganisationsListItem;
