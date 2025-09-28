import { Alert, AlertTitle, Box, Button, Chip, Collapse, Divider, Stack, Tooltip, Typography } from '@mui/material'
import React, { useCallback, useState } from 'react'
import ContentCopyIcon from "@mui/icons-material/ContentCopy";
import SupportAgentIcon from "@mui/icons-material/SupportAgent";
import { AuthErrorDetails } from '../types/AuthErrorDetails';
import { useNavigate } from 'react-router-dom';

interface ProcessErrorDetailsProps {
    details: AuthErrorDetails;
    onRetry?: () => void;
    onHome?: () => void;
}

export const ProcessErrorDetails: React.FC<ProcessErrorDetailsProps> = ({
    details,
    onRetry,
    onHome,
}) => {
    const navigate = useNavigate();
    const [open, setOpen] = useState<boolean>(false);

    const copyDetails = async () => {
        try {
            await navigator.clipboard.writeText(JSON.stringify(details, null, 2));
        } catch { }
    };

    const contactSupport = () => {

    };

    const handleHome = useCallback(() => {
        navigate("/");
    }, [])

    return (
        <Box
            sx={{
                width: "min(720px, 92vw)",
                minHeight: "100vh",
                mx: "auto",
                display: "grid",
                placeItems: "center",
            }}
        >
            <Box sx={{ width: "100%" }}>
                <Alert variant="outlined" severity="error" sx={{ p: 2.5 }}>
                    <AlertTitle sx={{ fontSize: 22, fontWeight: 700, width: "min(720px, 90vw)" }}>
                        {details.title ?? "Erro ao autenticar"}
                    </AlertTitle>
                    <Typography sx={{ mb: 2 }}>
                        {details.description ?? "Ocorreu um erro ao processar a autenticação."}
                    </Typography>

                    <Stack direction="row" spacing={1} useFlexGap flexWrap="wrap" sx={{ mb: 1 }}>
                        {onRetry && (
                            <Button variant="contained" onClick={onRetry}>
                                Tentar novamente
                            </Button>
                        )}

                        <Button variant="text" onClick={onHome ?? handleHome}>
                            Voltar para a página inicial
                        </Button>

                        <Button variant="text" startIcon={<SupportAgentIcon />} onClick={contactSupport}>
                            Contatar suporte
                        </Button>
                        <Tooltip title="Copiar detalhes">
                            <Button variant="text" startIcon={<ContentCopyIcon />} onClick={copyDetails}>
                                Copiar detalhes
                            </Button>
                        </Tooltip>
                    </Stack>

                    <Stack direction="row" spacing={1} sx={{ mb: 1 }}>
                        {details.traceId && <Chip size="small" label={`ID suporte: ${details.traceId}`} />}
                    </Stack>

                    <Divider sx={{ my: 1.5 }} />

                    <Button size="small" onClick={() => setOpen((o) => !o)}>
                        {open ? "Ocultar detalhes técnicos" : "Ver detalhes técnicos"}
                    </Button>
                    <Collapse in={open} unmountOnExit>
                        <Box
                            component="pre"
                            sx={{
                                mt: 1.5,
                                p: 1.5,
                                bgcolor: (t) => t.palette.action.hover,
                                borderRadius: 1,
                                whiteSpace: "pre-wrap",
                                overflowX: "auto",
                                fontSize: 13,
                            }}
                        >
                            {JSON.stringify(details, null, 2)}
                        </Box>
                        {details.errorUri && (
                            <Typography sx={{ mt: 1 }}>
                                Referência:{" "}
                                <a href={details.errorUri} target="_blank" rel="noreferrer">
                                    {details.errorUri}
                                </a>
                            </Typography>
                        )}
                    </Collapse>
                </Alert>
            </Box>
        </Box>
    );
};