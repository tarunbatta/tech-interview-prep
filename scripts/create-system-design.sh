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

# Prepare the new table row (without leading row number)
NEW_ROW="**${DISPLAY_NAME}** | Core architecture, scalability, data model, high availability | [${DISPLAY_NAME} Design](${SYSTEM_NAME}.md) |"

# Extract table rows under Interview System Design Case Studies and renumber
header_pattern='^## (Systems|Interview System Design)'
before_table=$(awk -v pat="$header_pattern" '
  $0 ~ pat { in_sec=1; print; next }
  in_sec && /^\| :---/ { print; exit }
  { print }
' "$SYSTEM_DESIGN_MD")

rows=$(awk -v pat="$header_pattern" '
  $0 ~ pat { in_sec=1; next }
  in_sec && /^\| [0-9]+ \|/ {
    sub(/^\| [0-9]+ \| /, "")
    print
    next
  }
  in_sec && /^---/ { exit }
' "$SYSTEM_DESIGN_MD")

after_table=$(awk -v pat="$header_pattern" '
  $0 ~ pat { in_sec=1; next }
  in_sec && /^---/ { found_end=1 }
  found_end { print }
' "$SYSTEM_DESIGN_MD")

# Add the new row, sort alphabetically by system name, and renumber
sorted_rows=$(printf "%s\n%s\n" "$rows" "$NEW_ROW" | sed '/^$/d' | sort -f | awk '{printf "| %d | %s\n", NR, $0}')

# Update total count in section header
total_count=$(echo "$sorted_rows" | wc -l | tr -d ' ')
before_table=$(echo "$before_table" | sed -E "s/(## Interview System Design Case Studies \()[0-9]+( Systems\))/\1${total_count}\2/")

# Write back to the file
{
  echo "$before_table"
  echo "$sorted_rows"
  echo ""
  echo "$after_table"
} > "$SYSTEM_DESIGN_MD.tmp" && mv "$SYSTEM_DESIGN_MD.tmp" "$SYSTEM_DESIGN_MD"

echo "Created new system design: $TARGET_FILE and updated system-design.md."
