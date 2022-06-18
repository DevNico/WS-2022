import React from 'react';
import { Alert, AlertColor, Collapse, IconButton } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';

interface CustomAlertProps {
	severity?: AlertColor;
	closeTimeout?: number;
	closeIcon?: boolean;
	children?: any;
}

interface CustomAlertState {
	open: boolean;
}

export default class CustomAlert extends React.Component<
	CustomAlertProps,
	CustomAlertState
> {
	private closeTimeout: NodeJS.Timeout | null = null;
	private showTimeout: NodeJS.Timeout | null = null;

	public constructor(props: CustomAlertProps) {
		super(props);

		this.state = {
			open: false,
		};
	}

	private _show() {
		this.closeTimeout = setTimeout(
			this.hide.bind(this),
			this.props.closeTimeout || 10000
		);
		this.setState({ open: true });
	}

	public show(): void {
		if (this.showTimeout) return;

		if (this.closeTimeout) {
			this.hide();

			this.showTimeout = setTimeout(() => {
				this.showTimeout = null;
				this._show();
			}, 250);
		} else {
			this._show();
		}
	}

	public hide(): void {
		if (this.closeTimeout) {
			clearTimeout(this.closeTimeout);
			this.closeTimeout = null;
		} else if (this.showTimeout) {
			clearTimeout(this.showTimeout);
			this.showTimeout = null;
		}

		this.setState({ open: false });
	}

	public override render() {
		return (
			<Collapse
				in={this.state.open}
				style={{ width: 'max-content', margin: 'auto' }}
			>
				<Alert
					action={
						this.props.closeIcon === false ? null : (
							<IconButton
								aria-label='close'
								color='inherit'
								size='small'
								onClick={this.hide.bind(this)}
							>
								<CloseIcon fontSize='inherit' />
							</IconButton>
						)
					}
					sx={{ mb: 2, boxShadow: 5 }}
					severity={this.props.severity || 'success'}
				>
					{this.props.children}
				</Alert>
			</Collapse>
		);
	}
}
