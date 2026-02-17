@echo off
title Claude Code Local
set ANTHROPIC_BASE_URL=http://localhost:11434
set ANTHROPIC_API_KEY=ollama
claude --model qwen2.5-coder:7b
pause