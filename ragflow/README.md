# RAGFlow — Narrative Intelligence Suite

RAGFlow is deployed separately from the main stack.

## Quick start

```bash
cd ragflow
docker compose up -d
```

Access the UI at `http://localhost:80` (or `https://localhost:443`).

API available at `http://localhost:9380`.

## Configuration

Set `RAGFLOW_API_KEY` in the environment or in the root `.env` file.

## API endpoints used by the backend

- `POST /api/v1/datasets` — create dataset for brand guidelines
- `POST /api/v1/datasets/{id}/documents` — upload brand documents
- `POST /api/v1/chats` — create chat session
- `POST /api/v1/chats/{id}/completions` — generate content with RAG
