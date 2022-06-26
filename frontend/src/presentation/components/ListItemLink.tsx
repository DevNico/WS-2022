import ListItem, { ListItemProps } from '@mui/material/ListItem/ListItem';
import ListItemIcon from '@mui/material/ListItemIcon/ListItemIcon';
import ListItemText from '@mui/material/ListItemText/ListItemText';
import React from 'react';
import { Link, LinkProps, useMatch } from 'react-router-dom';

type ListItemLinkProps = ListItemProps &
	Omit<LinkProps, 'to'> & {
		to: { $: string };
		text: string;
		disableMatch?: boolean;
		icon?: React.ReactElement;
	};

const ListItemLink: React.FC<ListItemLinkProps> = ({
	to,
	text,
	icon,
	disableMatch,
	...other
}) => {
	let link = to.$;
	if (!link.startsWith('/')) {
		link = `/${link}`;
	}

	const hasMatch = useMatch({ path: link, end: false }) !== null;

	return (
		<ListItem
			button
			component={Link as any}
			to={`/${to.$}`}
			selected={!(disableMatch ?? false) && hasMatch}
			{...other}
		>
			{icon && <ListItemIcon>{icon}</ListItemIcon>}
			<ListItemText primary={text} />
		</ListItem>
	);
};

export default ListItemLink;
