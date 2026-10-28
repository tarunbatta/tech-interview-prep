#!/bin/bash

# Usage: ./create-system-design.sh <system-name>

if [ -z "$1" ]; then
  echo "Usage: $0 <system-name>"
  exit 1
fi

SYSTEM_NAME="$1"
TEMPLATE_DIR="$(dirname "$0")/template"
TARGET_FILE="$(dirname "$0")/../system-design/$SYSTEM_NAME.md"
SYSTEM_DESIGN_MD="$(dirname "$0")/../system-design/system-design.md"

if [ -f "$TARGET_FILE" ]; then
  echo "Error: $TARGET_FILE already exists."
  exit 1
fi

# Copy template and substitute system name
cp "$TEMPLATE_DIR/template.md" "$TARGET_FILE"
sed -i '' "s/template\b/$SYSTEM_NAME/gI" "$TARGET_FILE"

# Prepare display name (title case, replace - and _ with space, capitalize each word)
DISPLAY_NAME=$(echo "$SYSTEM_NAME" | sed -E 's/[-_]/ /g' | awk '{for(i=1;i<=NF;i++){ $i=toupper(substr($i,1,1)) substr($i,2) }}1')

# Prepare the new entry
NEW_ENTRY="[${DISPLAY_NAME}](${SYSTEM_NAME}.md)"

# Extract list section and renumber
header_pattern='^## (Systems|Interview System Design)'
before_list=$(awk -v pat="$header_pattern" '$0 ~ pat {print; exit} {print}' "$SYSTEM_DESIGN_MD")
list=$(awk -v pat="$header_pattern" '$0 ~ pat {flag=1; next} flag && /^[0-9]+\. / {print}' "$SYSTEM_DESIGN_MD")
after_list=$(awk -v pat="$header_pattern" '$0 ~ pat {flag=1; next} flag && !/^[0-9]+\. / {print}' "$SYSTEM_DESIGN_MD" | tail -n +2)

# Add the new entry (without number)
list=$(echo -e "$list\n$NEW_ENTRY")

# Strip all leading numbers and dots, sort, and renumber
sorted_list=$(echo "$list" | sed 's/^[0-9][0-9]*\. *//' | sort -f | awk '{printf "%d. %s\n", NR, $0}')

# Write back to the file
{
  echo "$before_list"
  echo ""
  echo "$sorted_list"
  echo "$after_list"
} > "$SYSTEM_DESIGN_MD.tmp" && mv "$SYSTEM_DESIGN_MD.tmp" "$SYSTEM_DESIGN_MD"

echo "Created new system design: $TARGET_FILE and updated system-design.md."
