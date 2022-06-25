import CardActionArea, {
	CardActionAreaProps,
} from '@mui/material/CardActionArea';
import React from 'react';
import { Link, LinkProps } from 'react-router-dom';

interface RouterCardActionAreaProps
	extends Omit<CardActionAreaProps, keyof LinkProps | 'ref'> {
	to: { $: string };
	absolute?: boolean;
	replace?: boolean;
	children?: React.ReactNode;
}

const RouterCardActionArea: React.FC<RouterCardActionAreaProps> = ({
	to,
	replace,
	children,
	absolute,
	...rest
}) => {
	let link = to.$;
	if ((absolute ?? true) && !link.startsWith('/')) {
		link = `/${link}`;
	}

	return (
		<CardActionArea component={Link} to={link} {...rest}>
			{children}
		</CardActionArea>
	);
};

export default RouterCardActionArea;
