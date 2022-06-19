import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import ListItemButton from '@mui/material/ListItemButton/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon/ListItemIcon';
import PersonIcon from '@mui/icons-material/Person';
import ListItemText from '@mui/material/ListItemText/ListItemText';
import { ExpandLess, ExpandMore } from '@mui/icons-material';
import { Collapse } from '@mui/material';
import List from '@mui/material/List/List';
import PersonAddIcon from '@mui/icons-material/PersonAdd';

const RolesListItem: React.FC<{ isSuperAdmin: boolean }> = ({
	isSuperAdmin,
}) => {
	const [open, setOpen] = useState(true);
	const navigate = useNavigate();
	const { t } = useTranslation();
	const location = useLocation();

	const isRolesCreatePage = location.pathname === '/roles/create';
	const isRolesPage = location.pathname === '/roles';

	return (
		(isSuperAdmin && (
			<>
				<ListItemButton onClick={() => setOpen(!open)}>
					<ListItemIcon>
						<PersonIcon />
					</ListItemIcon>
					<ListItemText primary={t('homeLayout.roles')} />
					{open ? <ExpandLess /> : <ExpandMore />}
				</ListItemButton>
				<Collapse in={open} timeout='auto' unmountOnExit>
					<List component='div' disablePadding>
						<ListItemButton
							sx={{ pl: 4 }}
							selected={isRolesPage}
							onClick={() => navigate('/roles')}
						>
							<ListItemIcon>
								<PersonIcon />
							</ListItemIcon>
							<ListItemText primary={t('homeLayout.listRoles')} />
						</ListItemButton>
						<ListItemButton
							sx={{ pl: 4 }}
							selected={isRolesCreatePage}
							onClick={() => navigate('/roles/create')}
						>
							<ListItemIcon>
								<PersonAddIcon />
							</ListItemIcon>
							<ListItemText
								primary={t('homeLayout.createRole')}
							/>
						</ListItemButton>
					</List>
				</Collapse>
			</>
		)) || <></>
	);
};

export default RolesListItem;
